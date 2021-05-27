import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({providedIn: 'root'})
export class ValidatedHttpClient {
  constructor(private http: HttpClient) {
  }

  /** @returns promise promise that resolves the body of the response if 2XX , otherwise returns status code */
  public get<T>(url: string): Promise<T> {
    return new Promise((resolve, reject) => {
      this
        .http
        .get<T>(url, {observe: 'response'})
        .subscribe(value => {
          resolve(value.body);
        }, error => {
          reject(error.status);
        });
    });
  }

  /** @returns promise promise that resolves the body of the response if 2XX , otherwise returns status code */
  public post<T>(url: string, body: any): Promise<T> {
    return new Promise((resolve, reject) => {
      this
        .http
        .post<T>(url, body, {observe: 'response'})
        .subscribe(value => {
          resolve(value.body);
        }, error => {
          reject(error.status);
        });
    });
  }

  /*
  Usage example :
      this
      .validatedHttpClient
      .get<Document[]>(url)
      .then(docs => {
        docs.forEach(doc => {
          console.log(doc);
        });
      }, statusCode => {
        console.log(statusCode);
      });
   */

}
