import { Guid } from "guid-typescript"

export class Contact {
    Id :Guid | null;
    
    Name:string;
    
    Email:string; 
    
    PhoneNumber:string;
    
    UserId:Guid | null; 

    }

