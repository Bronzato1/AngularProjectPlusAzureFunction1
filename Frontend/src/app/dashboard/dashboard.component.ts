import { Component } from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

  constructor(private dataService: DataService) { }

  callBackend()
  {
    this.dataService.getMessage().subscribe((message: string) => {
       alert(message);
    })
  }
}
