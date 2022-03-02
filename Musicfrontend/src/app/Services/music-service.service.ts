import { Injectable } from '@angular/core';
import { HttpClient ,HttpParams  } from '@angular/common/http';
import { ITrackCollection } from '../Interfaces/ITrackCollection';
import { IAccesToken } from '../Interfaces/IAccesToken';
import { IPlaylist } from '../Interfaces/IPlaylist';
import { IPaggingWrapper } from '../Interfaces/IPaggingWrapper';
@Injectable({
  providedIn: 'root'
})
export class MusicServiceService {

  constructor(private http: HttpClient) { }
  MusicApiURL : string = "https://localhost:5001";
  AccessTokens : IAccesToken[] = []
  GetAccessToken(code : string)
  {
    return this.http.post(`${this.MusicApiURL}}`,null)
  }
  GetTrackList(id : number, page : number, pageSize : number, playlistIds : number[])
  {
    let queryParameters :string = "";
    let list = "";
    console.log(playlistIds.length)
    if(playlistIds.length > 0)
    {
       list = "&playlistIds=" + playlistIds.join("&playlistIds=")
    }

    return this.http.post<IPaggingWrapper>(`${this.MusicApiURL}/api/Music/user/${id}/getalltracks?page=${page}&pageSize=${pageSize}${list}`, this.AccessTokens)
  }
  GetPlaylists(id : number)
  {
    return this.http.get<IPlaylist[]>(`${this.MusicApiURL}/api/Playlist/user/${id}/getallplaylists`)
  }
}
