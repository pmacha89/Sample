import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { style } from '@angular/animations';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public sortedTripList: Array<{ id: number, summary: string }> = [];
  public tripList: string[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string[]>(baseUrl + 'TripSort').subscribe(result => {
      this.tripList = result;
      let index = 0;
      for (var trip of this.tripList) {
        debugger;
        let tripItem = new Trip();
        tripItem.id = index++;
        tripItem.summary = trip;
        this.sortedTripList.push({ id: index++, summary:trip});
      }
      console.log(this.sortedTripList);
      //this.forecasts = result;
    }, error => console.error(error));
  }
}

export class Trip {
  id: number;
  summary: string;
}
