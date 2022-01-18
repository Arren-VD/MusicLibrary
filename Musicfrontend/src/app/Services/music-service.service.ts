import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MusicServiceService {

  constructor(private http: HttpClient) { }
  MusicApiURL : string = "https://localhost:5001";
  GetAccessToken(code : string)
  {
    return this.http.post(`${this.MusicApiURL}}`,null)
  }
}
