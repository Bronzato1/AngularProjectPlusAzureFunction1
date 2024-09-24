import { Component, Inject, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { OktaAuth } from '@okta/okta-auth-js';
import { OktaAuthStateService, OKTA_AUTH } from '@okta/okta-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit
{
  title = 'AngularProjectPlusAzureFunction1';

  isAuthenticated: boolean = false;

  constructor(
    private dataService: DataService, 
    @Inject(OKTA_AUTH) public oktaAuth: OktaAuth,
    private authStateService: OktaAuthStateService
  ) { }

  async ngOnInit()
  {
    this.isAuthenticated = await this.oktaAuth.isAuthenticated();
    this.authStateService.authState$.subscribe(
      (response) => {
          this.isAuthenticated = response.isAuthenticated ?? false;
      }
    );    
  }

  login()
  {
    this.oktaAuth.signInWithRedirect();
  }

  logout()
  {
    this.oktaAuth.signOut();
  }

  callBackend()
  {
    this.dataService.getMessage().subscribe((message: string) => {
       alert(message);
    })
  }
}
