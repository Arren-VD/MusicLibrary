import { IPlaylist } from '../Interfaces/IPlaylist';
import { IArtist } from '../Interfaces/IArtists';
export interface ITrackListCollection {
    id: number;
    name: string;
    isrC_Id: string;
    playlists: IPlaylist[];
    artists: IArtist[];
}