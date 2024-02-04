import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { MasterCompanyApiResponse } from 'src/app/models/masterCompanyApi.model';

@Injectable({
  providedIn: 'root'
})
export class MasterCompanyApiService {
  url: string = "https://localhost:7089/Employee";
  constructor(private _http: HttpClient) { 

  }

  getEmployees():Observable<MasterCompanyApiResponse>{
    return this._http.get<MasterCompanyApiResponse>(this.url);
  }
}
