import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlaylist } from '../Interfaces/IPlaylist';
import { IArtist } from '../Interfaces/IArtists';
import { IClientPlayList } from '../Interfaces/IClientPlaylist';
import { ITrackListCollection } from '../Interfaces/ITrackCollection';
import { MusicServiceService } from '../Services/music-service.service';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {
  constructor(  private route: ActivatedRoute,private router: Router,private tracklistSvc : MusicServiceService,private authSvc : SpotfiyAuthenticationService) { }
  id : any;
  tracklist : any;
  playlists : any;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log("Acces Token for Get Track :   "+ this.tracklistSvc.AccessTokens);
      console.log(this.tracklistSvc.AccessTokens);
      this.tracklistSvc.GetPlaylists(this.id).subscribe(x =>{this.playlists = x, console.log(x)});
      this.tracklistSvc.GetTrackList(this.id).subscribe(x =>{this.tracklist = x, console.log(x)});
    })
  }
  TracklistContains(clientPlaylists : IClientPlayList[],serviceName : string)
  {
    console.log(clientPlaylists)
    console.log(serviceName)
    if(clientPlaylists.filter(x => x.clientServiceName == serviceName).length > 0)
    {
      return true;      
    }
    else
    {
      return false;
    }
  }
}
