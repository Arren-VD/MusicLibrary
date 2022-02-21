import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlaylist } from '../Interfaces/IPlaylist';
import { IArtist } from '../Interfaces/IArtists';
import { IClientPlayList } from '../Interfaces/IClientPlaylist';
import { ITrackListCollection } from '../Interfaces/ITrackCollection';
import { MusicServiceService } from '../Services/music-service.service';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';
import {ICheckboxValue } from '../Interfaces/ICheckboxValue'

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
  page : any = 1;
  pageSize : any = 50;
  playlistIds : number[] =  [];
  playlistCheckboxValues : ICheckboxValue[] = [];

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log("Acces Token for Get Track :   "+ this.tracklistSvc.AccessTokens);
      console.log(this.tracklistSvc.AccessTokens);
      this.tracklistSvc.GetPlaylists(this.id).subscribe(x =>{
        this.playlists = x
        x.forEach(y => this.playlistCheckboxValues.push({id : y.id, label : y.name, isChecked : false}))
      });
      this.GetTracks()
    })
  }
  TracklistContains(clientPlaylists : IClientPlayList[],serviceName : string)
  {
    if(clientPlaylists.filter(x => x.clientServiceName == serviceName).length > 0)
    {
      return true;      
    }
    else
    {
      return false;
    }
  }
  fetchCheckedIDs() {
    this.playlistCheckboxValues = []
    this.playlistCheckboxValues.forEach((value, index) => {
      if (value.isChecked) {
        this.playlistCheckboxValues.push(value.id);
      }
    });
  }
  changeSelection(item : ICheckboxValue) 
  {
    if(item.isChecked)
      this.playlistIds.push(item.id)
    else
    this.playlistIds = this.playlistIds.filter(x => x != item.id)
    this.GetTracks()
  }
  GetTracks()
  {
    this.tracklistSvc.GetTrackList(this.id,this.page,this.pageSize,this.playlistIds).subscribe(x =>{this.tracklist = x});
  }
}
