import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.scss']
})
export class SendEmailComponent implements OnInit {

  emails: any;

  constructor(private http: HttpClient) { 
    
  }

  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/email').subscribe(response => {
      this.emails = response;
    });
  }

}
