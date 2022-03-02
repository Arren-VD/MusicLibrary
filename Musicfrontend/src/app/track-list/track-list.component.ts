import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlaylist } from '../Interfaces/IPlaylist';
import { IArtist } from '../Interfaces/IArtists';
import { IClientPlayList } from '../Interfaces/IClientPlaylist';
import { ITrack } from '../Interfaces/ITrack';
import { MusicServiceService } from '../Services/music-service.service';
import { SpotfiyAuthenticationService } from '../Services/spotfiy-authentication.service';
import {ICheckboxValue } from '../Interfaces/ICheckboxValue'
import { HttpParams  } from '@angular/common/http';

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.css']
})
export class TrackListComponent implements OnInit {
  constructor(  private route: ActivatedRoute,private router: Router,private tracklistSvc : MusicServiceService,private authSvc : SpotfiyAuthenticationService) { }
  id : any;
  tracklist : ITrack[] = [];
  playlists : IPlaylist[] = [];
  page : any = 1;
  pageSize : any = 50;
  playlistIds : number[] =  [];
  playlistCheckboxValues : ICheckboxValue[] = [];
  trackNumbers : any;
  totalPages : any;
  queryParams : any;
  filterRefreshTimer : any;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.page = params['page'];
      this.route.queryParamMap.subscribe((queryParams: any) => 
      {
        this.queryParams = JSON.stringify(queryParams);
        if(queryParams.params.playlistIds != undefined)
          this.playlistIds = JSON.parse(queryParams.params.playlistIds)
        this.tracklistSvc.GetPlaylists(this.id).subscribe(x =>{
          this.playlistCheckboxValues = [];
          this.playlists = x
          Array.from(this.playlists).forEach(y => 
            {
              if(this.playlistIds?.find(z => z == y.id) != undefined)
                this.playlistCheckboxValues.push({id : y.id, label : y.name,isChecked : true})
              else
            this.playlistCheckboxValues.push({id : y.id, label : y.name,isChecked : false})
          })
        });
        this.GetTracks()
      });
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
    this.filterRefreshTimer = setInterval(() => {
      this.ApplyFiltersAndChangePage();
    },2000)
  }
  ApplyFiltersAndChangePage()
  {
    this.router.navigate([`tracklist/user/1/page/1`], {
      queryParams: {
        playlistIds: JSON.stringify(this.playlistIds)
      }
    });
  }
  GetTracks()
  {
    this.tracklistSvc.GetTrackList(this.id,this.page,this.pageSize,this.playlistIds).subscribe(x =>{
      this.tracklist = x.collection
      console.log(x.totalPages)
      this.totalPages = x.totalPages;
      this.trackNumbers  =  this.ArrayFromXtoY(Math.max(this.page -3,1), Math.min(Number(this.page) + 3,this.totalPages))
    });
  }
  ArrayFromXtoY(lowEnd : number,highEnd : number)
  {
  var list = [];
  for (var i = lowEnd; i <= highEnd; i++) {
    list.push(i);
    }
    return list;
  }
  ngOnDestroy()
  {
    if (this.filterRefreshTimer) {
      clearInterval(this.filterRefreshTimer);
    }
  }
}
