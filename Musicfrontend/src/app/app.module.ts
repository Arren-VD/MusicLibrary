import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {HeaderInterceptor} from '../app/Interceptors/HeaderInterceptor'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TrackListComponent } from './track-list/track-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SpotifyLoginComponent } from './spotify-login/spotify-login.component';

@NgModule({
  declarations: [
    AppComponent,
    TrackListComponent,
    HomeComponent,
    NavBarComponent,
    SpotifyLoginComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    RouterModule.forRoot(
      [
        {path:"tracklist/:id",component:TrackListComponent},
        {path:"home",component:HomeComponent},
        {path:"spotifylogin",component:SpotifyLoginComponent}
      ]
    ),
  ],
  providers: [
    {
      provide:  
      HTTP_INTERCEPTORS,
      useClass : HeaderInterceptor,
       multi : true,

      }],
  bootstrap: [AppComponent]
})
export class AppModule { }
