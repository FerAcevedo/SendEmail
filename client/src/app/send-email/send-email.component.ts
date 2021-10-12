import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.scss']
})
export class SendEmailComponent implements OnInit {

  emails: any;
  emailModel: any= {};
  slaList: any;
  modalRef?: BsModalRef;
  responseCode: number | undefined;
  
  constructor(private http: HttpClient, private modalService: BsModalService) {     
  }
  
  ngOnInit(): void {
    this.getSLAs();
  }

  getSLAs(){
    this.http.get('http://localhost:5000/api/sla').subscribe(sla =>this.slaList = sla);
  }

  async saveAndSendEmail(template: TemplateRef<any>){
      await this.http.post('http://localhost:5000/api/email/send', this.emailModel)
      .toPromise()
      .then(post => this.emailModel = post);
      //create xml and send email ticket, sla and file
      await this.http.post<number>('http://localhost:5000/api/EmailSender/emailsend', this.emailModel)
      .toPromise()
      .then(code => this.responseCode = code);
      //thank you message
      this.modalRef = this.modalService.show(template);
      this.emailModel = {};

    }
}
