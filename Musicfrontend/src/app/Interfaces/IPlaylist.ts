import { IClientPlayList } from '../Interfaces/IClientPlaylist';
export interface IPlaylist {
    id: number;
    clientPlayList: IClientPlayList[];
    name: string;
}