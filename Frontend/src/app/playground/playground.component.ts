import { Component, OnInit } from '@angular/core';
import { firstValueFrom, Observable, of } from 'rxjs';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrl: './playground.component.css'
})
export class PlaygroundComponent implements OnInit {

  test1$: Observable<string> = new Observable(observer => observer.next('abc'));
  test2$: Observable<string> = of ('xyz');

  value1!: string;
  
  async ngOnInit() {
    this.value1 = await firstValueFrom(this.test1$, {defaultValue: 'Some default value'});
    alert(this.value1);
  }
}
