





/// <reference path="Enums.ts" />

declare module Models {
    interface List {
        
        OwnerGender: string;
        PetNames: string[];
    }
}

declare module Models.ViewModel {
    interface JSONReturnVM<T> {
        element: T;
        errormessage: string;
        haserror: boolean;
    }
}