import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
  constructor(public authSvc: SpotfiyAuthenticationService) { }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let reqUrl = httpRequest.url;
    console.log(reqUrl)
    if(reqUrl.indexOf('5001')!== -1)
    {
      console.log("MusicAPI Call");
      return next.handle(
        httpRequest.clone({
            setHeaders: {
              "Authorization" : `Bearer ${this.authSvc.apiKey?.access_token}`,
              // "Content-Type": "application/x-www-form-urlencoded"
               }}))
    }
    else(reqUrl.indexOf('accounts.spotify.com')!== -1)
    {
      console.log("SpotifyAPI Call");
      return next.handle(
        httpRequest.clone({
            setHeaders: {
              "Authorization" : `Basic ${ btoa(this.authSvc.client_id + ":" + this.authSvc.secret)}`,
              //"Authorization" : `Basic ${  (str: string):string => Buffer.from(this.authSvc.client_id + ":" + this.authSvc.secret, 'binary').toString('base64')}`,
               "Content-Type": "application/x-www-form-urlencoded"
               }}))
    }
  }
}