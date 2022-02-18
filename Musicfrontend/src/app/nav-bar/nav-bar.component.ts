import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';
import { MusicServiceService } from '../Services/music-service.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public spotAuth : SpotfiyAuthenticationService,private route: ActivatedRoute, private mscSvc : MusicServiceService) { }

  ngOnInit() {

  }
  id : any = 1;
  title = 'MusicFrontend';

  login()
  {
    window.open(`https://accounts.spotify.com/authorize/?client_id=${this.spotAuth.client_id}&response_type=code&scope=${this.spotAuth.scope}&redirect_uri=http:%2F%2Flocalhost:4200/spotifylogin`,"_self")
  }
  loginTest()
  {
    let a = this.mscSvc.GetTrackList(this.id).subscribe(x => console.log(x), (error) => console.log(console.error()));
  }
}
