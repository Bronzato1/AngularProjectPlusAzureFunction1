import { provideHttpClient } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from "../environments/environment";
import { OktaAuthModule, OktaConfig, OktaAuthGuard, OktaCallbackComponent } from '@okta/okta-angular';
import { OktaAuth } from '@okta/okta-auth-js';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { appRoutes } from './app.routes';

const authConfig = {
  issuer: `https://${environment.devOktaDomain}/oauth2/default`,
  clientId: environment.clientId,
  redirectUri: environment.redirectUri
};

const oktaAuth = new OktaAuth(authConfig);
const moduleConfig: OktaConfig = { oktaAuth };

@NgModule({
  declarations: [AppComponent],
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
