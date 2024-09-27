import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  encapsulation: ViewEncapsulation.None
})
export class AppComponent { 
  title = 'Appli 321';

  isAuthenticated: boolean = false;

  constructor(private modalService: NgbModal) { }

  public open(modal: any): void {
    this.modalService.open(modal);
  }
}
