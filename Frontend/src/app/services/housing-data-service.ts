import { Injectable } from '@angular/core';
import { IHousingLocation } from '../interfaces/housing.location.interface';

@Injectable({
  providedIn: 'root'
})
export class HousingService {
  url = 'http://localhost:3000/locations';

  constructor() { }

  async getAllHousingLocations(): Promise<IHousingLocation[]> {
    const data = await fetch(this.url);
    return await data.json() ?? [];
  }

  async getHousingLocationById(id: Number): Promise<IHousingLocation | undefined> {
    const data = await fetch(`${this.url}/${id}`);
    return await data.json() ?? {};
  }
}


