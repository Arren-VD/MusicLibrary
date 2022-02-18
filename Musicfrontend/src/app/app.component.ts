import { Component, OnInit } from '@angular/core';
import { SpotfiyAuthenticationService } from "../app/Services/spotfiy-authentication.service";
import {ActivatedRoute} from '@angular/router';
import { empty } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  constructor()
  {

  }
  ngOnInit()
  {
 
  }

}
