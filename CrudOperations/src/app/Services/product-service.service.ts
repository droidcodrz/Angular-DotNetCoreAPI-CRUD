import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from './product';

@Injectable({
  providedIn: 'root'
})
export class ProductServiceService {

  private url:string ="https://localhost:5001/api/Home/";
  constructor(private httpClient:HttpClient) { }

 public getAllProducts():Observable<Product[]>
  {
    return this.httpClient.get<Product[]>(this.url+"GetAllProduct");
  }

  public getAllProductsP():Promise<any>
  {
        var response=new Promise((resolve, reject) => {
      this.httpClient.get<Product[]>(this.url+"GetAllProduct")
      .toPromise()
      .then(
        success => { // Success
          console.log(success);
          resolve(success);
        },
        err=>{
          //reject
          console.log(err);
          reject(err);
        }
    );
  });
  return response;
}
public saveProduct(product:Product):Observable<boolean>
  {   
    return this.httpClient.post<boolean>(this.url+"AddProduct",product);
  }
  public removeProduct(productId:number):Observable<boolean>
  {   
    return this.httpClient.delete<boolean>(this.url+"DeleteProduct/"+productId);
  }

  public updateProduct(product:Product):Observable<boolean>
  {   
    return this.httpClient.put<boolean>(this.url+"UpdateProduct",product);
  }

}
