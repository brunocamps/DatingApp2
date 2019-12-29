import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html', // html template
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit { // implements OnInit interface
  values: any;

  constructor(private http: HttpClient) { } // constructor goes first. Inject the service you want to use


  ngOnInit() { // when our component initializes, we call method getValues and populate with response back from the server
    this.getValues();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      // declare class property of what we're going to store
      this.values = response; // response
    }, error => {
      console.log(error);
    }); // give roots from API endpoint
    // subscribe to the observable above
  }

}
