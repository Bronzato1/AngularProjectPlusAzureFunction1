import { provideHttpClient } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DetailsComponent } from './details/details.component';
import { environment } from "../environments/environment";
import { OktaAuthModule, OktaConfig, OktaAuthGuard, OktaCallbackComponent } from '@okta/okta-angular';
import { OktaAuth } from '@okta/okta-auth-js';
import { PlaygroundComponent } from './playground/playground.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

const authConfig = {
  issuer: `https://${environment.devOktaDomain}/oauth2/default`,
  clientId: environment.clientId,
  redirectUri: environment.redirectUri
};

const oktaAuth = new OktaAuth(authConfig);
const moduleConfig: OktaConfig = { oktaAuth };

const appRoutes: Routes = [ 
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'playground', component: PlaygroundComponent },
  { path: 'details', component: DetailsComponent, canActivate: [OktaAuthGuard] },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'logout/callback', component: OktaCallbackComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DetailsComponent,
    PlaygroundComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    OktaAuthModule.forRoot(moduleConfig),
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
