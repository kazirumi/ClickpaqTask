import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import {  inject, TemplateRef } from '@angular/core';

import { ModalDismissReasons, NgbActiveModal, NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';
import { ContactService } from '../shared/contact.service';
import { Contact } from '../shared/contact.model';

@Component({
  selector: 'contact-list',
  templateUrl: './contact-list.component.html',
  styles: []
})
export class ContactListComponent implements OnInit {

  constructor(public contactService:ContactService,public toastr:ToastrService) { }

  ngOnInit(): void {
    this.contactService.getAllContact();
  }

  get f(){
    return this.contactService.contactModel.controls;
  }

  deleteContact(contact_id:string){
    
    let allow=confirm("Are you sure to delete");
    
    if(allow)
    this.contactService.deleteContact(contact_id).subscribe(
      (res:any)=>{
          this.toastr.success('Contact deleted','Delete Successful');
          this.contactService.getAllContact();
         

      },
      err=>{
        console.log(err);
      }
    );
  }


  private modalService = inject(NgbModal);

 modalRef:NgbActiveModal;
  onSubmit(content:TemplateRef<any>){
    if(this.contactService.contactModel.value.Id){
          this.contactService.editContact().subscribe(res=>{
            this.toastr.success(' Contact Edit','Edit Successful');
            this.contactService.getAllContact();
            this.modalRef.close();
            this.contactService.contactModel.reset();

          },
          (err) => {
            console.log('Unable Edit Contact');
          }
        );
    }else{
      this.contactService.createContact().subscribe(res=>{
        this.toastr.success(' Contact Create','Created Successful');
        this.contactService.getAllContact();
        this.modalRef.close();
        this.contactService.contactModel.reset();

      },
      (err) => {
        console.log('Unable Crate Contact');
      });
    }
     

  }

  onCreateContact(content: TemplateRef<any>){
    this.openModal(content);
    this.contactService.contactModel.reset();
    this.contactService.contactModel.markAsPristine();
    this.contactService.contactModel.markAsUntouched();
    this.contactService.contactModel.updateValueAndValidity();

  
  }

  onEditContact(content: TemplateRef<any>,contact:Contact){
    this.openModal(content);
    this.contactService.contactModel.patchValue({
      Id:contact.Id,
      Name:contact.Name,
      PhoneNumber:contact.PhoneNumber,
      Email:contact.Email
    });
  }
	openModal(content: TemplateRef<any>) {
		this.modalRef=this.modalService.open(content, { centered: true ,backdrop:false});
	}


}
