import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {HeaderInterceptor} from '../app/Interceptors/HeaderInterceptor'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide:  
      HTTP_INTERCEPTORS,
      useClass : HeaderInterceptor,
       multi : true
      }],
  bootstrap: [AppComponent]
})
export class AppModule { }
