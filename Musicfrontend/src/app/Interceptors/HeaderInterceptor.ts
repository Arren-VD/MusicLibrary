import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
  constructor(public authSvc: SpotfiyAuthenticationService) { }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let reqUrl = httpRequest.url;
    if(reqUrl.indexOf('5001')!== -1)
    {
      return next.handle(
        httpRequest.clone({
            setHeaders: {
              //"Authorization" : `Bearer ${this.authSvc.apiKey?.access_token}`,
               "Content-Type": "application/json",
              "Access-Control-Allow-Origin": "*",
              "Access-Control-Allow-Methods": "GET,HEAD,OPTIONS,POST,PUT",
              "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept, Authorization"
               }}))
    }
    else(reqUrl.indexOf('accounts.spotify.com')!== -1)
    {
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