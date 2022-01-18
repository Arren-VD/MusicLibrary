import { Component, OnInit } from '@angular/core';
import { SpotfiyAuthenticationService } from "../app/Services/spotfiy-authentication.service";
import {ActivatedRoute} from '@angular/router';
import { empty } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  constructor(public spotAuth : SpotfiyAuthenticationService,private route: ActivatedRoute)
  {

  }
  ngOnInit()
  {
    let code;
    this.route.queryParams.subscribe(params   => {
      code = params.code
      this.spotAuth.GetAccessToken(code ).subscribe(x => {
        this.spotAuth.Login(x),
        console.log("Succesfully got Access Token")
        console.log(x)
    },
      (error) => {
        console.log("unsuccesfully got Access Token")
      console.log(error)
      }
      );
    })
  }
  title = 'MusicFrontend';

  login()
  {
    window.open(`https://accounts.spotify.com/authorize/?client_id=${this.spotAuth.client_id}&response_type=code&scope=${this.spotAuth.scope}&redirect_uri=http:%2F%2Flocalhost:4200`,"_self")
  }
  import()
  {

  }
  Refresh()
  {
    console.log(this.spotAuth.loggedIn$.getValue())
  }
}
