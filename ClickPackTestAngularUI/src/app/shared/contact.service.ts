import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import {environment}  from '../../environments/environment';
import { Contact } from './contact.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  constructor(private fb: FormBuilder, private http: HttpClient) {}
  // readonly rootURL = 'https://localhost:44358/api';

  ContactList: Contact[];

  contactModel = this.fb.group({
    Id:[],
    Name: ['', Validators.required],
    Email: [
      '',
      [
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
        // Validators.required,
      ],
    ],
    PhoneNumber: ['',[Validators.required,
      Validators.pattern("^[0-9]*$"),
      Validators.minLength(10), Validators.maxLength(10)]],
  });

  getAllContact() {
    return this.http.get(environment.rootURL + '/Contacts').subscribe(
      (res: any) => {
        this.ContactList = res as Contact[];
        console.log(this.ContactList);
      },
      (err) => {
        console.log('Unable to Collect Contact List');
      }
    );
  }

  deleteContact(Id: string) {
    return this.http.delete(environment.rootURL + '/Contacts/' + Id);
  }

  createContact() {
    var body=
    {
      
      Name:this.contactModel.value.Name,
      
      Email:this.contactModel.value.Email ,
      
      PhoneNumber:this.contactModel.value.PhoneNumber
  
      }
    
    return this.http.post(environment.rootURL + '/Contacts' ,body);
  }

  editContact() {
    var body={
      Id:this.contactModel.value.Id,
      Name:this.contactModel.value.Name,
      Email:this.contactModel.value.Email,
      PhoneNumber:this.contactModel.value.PhoneNumber
    }
    return this.http.put(environment.rootURL + '/Contacts/' + this.contactModel.value.Id,body);
  }
}
