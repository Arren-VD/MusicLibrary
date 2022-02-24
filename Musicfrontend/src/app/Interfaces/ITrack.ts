import { IPlaylist } from '../Interfaces/IPlaylist';
import { IArtist } from '../Interfaces/IArtists';
export interface ITrack {
    id: number;
    name: string;
    isrC_Id: string;
    playlists: IPlaylist[];
    artists: IArtist[];
}