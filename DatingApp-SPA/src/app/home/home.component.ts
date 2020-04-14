import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  values: any;

  // inject the HTTP CLIENT
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  registerToggle() {
    this.registerMode = true;
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

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
