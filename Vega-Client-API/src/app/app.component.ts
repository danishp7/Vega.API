import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: 'index.html',
  styles: []
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) { }
  
  title = 'Vega-Client-API';
  values: any;

  getValues() {
    this.http.get("http://localhost:5000/weatherforecast").subscribe(response => {
      this.values = response;
    });
  }
  ngOnInit(): void {
    this.getValues();
  }
}
