import { Component, Input, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ICustomer } from '../../interfaces/customer.interface';
import { Sorter } from '../../core/sorter';
import { TrackByService } from '../../core/trackby.service';
import { CommonModule } from '@angular/common';
import { CapitalizePipe } from '../../common/pipes/capitalize.pipe';
import { TrimPipe } from '../../common/pipes/trim.pipe';

@Component({ 
  selector: 'customers-grid', 
  standalone: true,
  imports: [RouterLink, CommonModule, CapitalizePipe, TrimPipe],
  templateUrl: './customers-grid.component.html',
  //When using OnPush detectors, then the framework will check an OnPush 
  //component when any of its input properties changes, when it fires 
  //an event, or when an observable fires an event ~ Victor Savkin (Angular Team)
  changeDetection: ChangeDetectionStrategy.OnPush 
})
export class CustomersGridComponent implements OnInit {

  @Input() customers: ICustomer[] = [];

  constructor(private sorter: Sorter, public trackby: TrackByService) { }
   
  ngOnInit() {

  }

  sort(prop: string) {
      this.sorter.sort(this.customers, prop);
  }

}
