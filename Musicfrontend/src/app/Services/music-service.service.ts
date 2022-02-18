import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITrackListCollection } from '../Interfaces/ITrackCollection';
import { IAccesToken } from '../Interfaces/IAccesToken';
import { IPlaylist } from '../Interfaces/IPlaylist';
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
  GetTrackList(id : number)
  {
    return this.http.post<ITrackListCollection>(`${this.MusicApiURL}/api/Music/user/${id}/getalltracks`, this.AccessTokens)
  }
  GetPlaylists(id : number)
  {
    return this.http.get<IPlaylist>(`${this.MusicApiURL}/api/Music/user/${id}/getallplaylists`)
  }
}
