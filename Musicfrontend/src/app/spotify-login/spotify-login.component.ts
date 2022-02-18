import { Component, OnInit } from '@angular/core';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';
import { ActivatedRoute } from '@angular/router';
import { MusicServiceService } from '../Services/music-service.service';

@Component({
  selector: 'app-spotify-login',
  templateUrl: './spotify-login.component.html',
  styleUrls: ['./spotify-login.component.css']
})
export class SpotifyLoginComponent implements OnInit {

  constructor(public spotAuthSvc: SpotfiyAuthenticationService, private route: ActivatedRoute,private mscSvc : MusicServiceService) { }

  ngOnInit() {
    if (this.spotAuthSvc.SpotifyCode == undefined) {
      this.route.queryParams.subscribe(params => {
        this.spotAuthSvc.SpotifyCode = params.code;
        this.GetAccessToken(params.code);
      });
    }
    else
    {
      this.GetAccessToken(this.spotAuthSvc.SpotifyCode);
    }
  }
  GetAccessToken(code: any) {
    this.spotAuthSvc.GetAccessToken(code).subscribe(x => {
      this.spotAuthSvc.Login(x),
        console.log("Succesfully got Access Token")
      console.log(x.access_token);
    },
      (error) => {
        console.log("Failed to get Access Token")
      }
    );

  }
}
