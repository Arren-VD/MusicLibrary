import { Injectable, ɵCodegenComponentFactoryResolver } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { IAccesToken } from '../Interfaces/IAccesToken';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpotfiyAuthenticationService {

  constructor(private http: HttpClient) { 
    
  }
  client_id : string = '33c6ecbc7ddf416599d017ec97d06205';
 redirect_uri : string = 'http:%2F%2Flocalhost:4200';
  //redirect_uri : string = 'http://localhost:4200';
 spotifyAuthorizeURL : string = "https://accounts.spotify.com/authorize"
 apiKey : IAccesToken | undefined;
 secret : string = "c71a2453e8e647c3a6f5f87783415d3f";
 //grant_type: string = 'client_credentials'
 grant_type: string = 'authorization_code'
 token_timer : any;
 scope : string = "user-read-email playlist-read-collaborative playlist-read-private user-read-private playlist-read-collaborative"

loggedIn$ = new BehaviorSubject(false);

  GetAccessToken(code : string)
  {
    let spot = {code : code, grant_type : this.grant_type,redirect_uri : this.redirect_uri,/* client_secret : this.secret,client_id : this.client_id*/};
    return this.http.post<IAccesToken>(`https://accounts.spotify.com/api/token`,`client_id=${this.client_id}&grant_type=authorization_code&code=${code}&redirect_uri=${this.redirect_uri}&client_secret=${this.secret}`)
  }
  Logout()
  {
    console.log("logging out")
    clearInterval(this.token_timer);
    this.loggedIn$.next(false);
    this.apiKey = undefined; 
  }
  Login(x : any)
  {
    console.log("logging in")
    clearInterval(this.token_timer);
    this.loggedIn$.next(true);
    this.apiKey = x; 
    this.token_timer = setInterval(() => { this.Logout()
    }, x.expires_in*1000)
  }
}