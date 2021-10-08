import { Component, OnInit } from '@angular/core';
import { ProductServiceService } from '../Services/product-service.service';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { orderBy, SortDescriptor, State } from '@progress/kendo-data-query';
import { Product } from '../Services/product';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public gridData: GridDataResult;
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10,
  };
  public skip=0;
  private editedRowIndex: number;
  public multiple = true;
  public allowUnsort = true;
  public editedProduct:Product;

  public sort: SortDescriptor[] = [
    {
      field: "ProductName",
      dir: "asc",
    },
  ];

  constructor(private productService:ProductServiceService) {
  
   }
  
   public onStateChange(state: State) {
    this.gridState = state;

    this.loadGrid();
  }
  public addHandler({ sender }, formInstance) {
    formInstance.reset();
    this.closeEditor(sender);
    sender.addRow(new Product());
  }

  public editHandler({ sender, rowIndex, dataItem }) {
    //sender is kend grid
    this.closeEditor(sender); //Close exisitng  editior
    this.editedRowIndex = rowIndex;
    this.editedProduct = Object.assign({}, dataItem);
    sender.editRow(rowIndex); //open row in edit mode
  }

  public cancelHandler({ sender, rowIndex }) {
    this.closeEditor(sender, rowIndex);
  }

  public saveHandler({ sender, rowIndex, dataItem, isNew }) {

    var product=new Product();
    product=Object.assign({},dataItem)
    if(isNew)
    {
    this.productService.saveProduct(product).subscribe(x=>{
      console.log(x)
      this.loadGrid();
    });
    }
    else
    {
      this.productService.updateProduct(product).subscribe(x=>{
        console.log(x);
        if(x)
        {
          this.loadGrid();
        }
      });
    }
   sender.closeRow(rowIndex);

    this.editedRowIndex = undefined;
    this.editedProduct = undefined;
  }

  public removeHandler({ dataItem }) {
    console.log(dataItem)
    this.productService.removeProduct(Number(dataItem.productID)).subscribe(x=>{
      console.log(x)
      if(x)
      {
        this.loadGrid();
      }
    });
  }

  private closeEditor(grid, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    //this.editService.resetItem(this.editedProduct);
    this.editedRowIndex = undefined;
    this.editedProduct = undefined;
  }

   public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.loadGrid();
  }

  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort;
    this.loadGrid();
  }
  
  loadGrid()
  {
    // this.productService.getAllProductsP().then((res)=>{   })

    this.productService.getAllProducts().subscribe(x=>{
      var result=x;
      this.gridData={data: orderBy(result.slice(this.skip, this.skip + 10),this.sort),
        total: result.length}
        console.log(this.gridData);
    });
    
  }

  public AddProduct(event)
  {
    var product=new Product;
    product.ProductName=event.ProductName;
    product.ProductModel=event.ProductModel;
    product.UnitPrice=event.UnitPrice;
    product.Discontinued=event.ProductInStock;
    console.log(event);
    console.log(product);
    this.productService.saveProduct(product).subscribe(x=>{
      this.loadGrid();
    });
    

  }

  ngOnInit() {
    
    this.loadGrid();
  }


}
