import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Base } from "./Base";
import { Degree } from "./Degree";

export class Candidate extends Base {
 degrees:Degree[] = [];
 email!:string;
 mobile?:number;
 cvBlob?:string;
 firstname!:string;
 lastname!:string;
 
 toFormGroup(fb:FormBuilder):FormGroup{
    return fb.group({
        id:[this.id],
        //max length for db
        firstname:[this.firstname,[Validators.required,Validators.maxLength(100)]],
        lastname: [this.lastname, [Validators.required]],
        email: [this.email, [Validators.required,Validators.email,Validators.maxLength(500)]],
        cvBlob: [this.cvBlob ],
        mobile: [this.mobile, [ Validators.maxLength(10),Validators.minLength(10)]],
        degrees: fb.array(this.degrees.map(x => x.toFormGroup(fb)) || [])
    })
    }

    toModel(obj:any):Candidate {
        this.firstname = obj.firstname;
        this.lastname =obj.lastname;
        this.email = obj.email;
        this.id = obj.id;
        this.cvBlob = obj.cvBlob;
        this.mobile = obj.mobile;
        this.degrees = obj.degrees.map((l:any) => new Degree().toModel(l));
        return obj;
    }
}
