export interface MasterCompanyApiResponse{
    employees: MasterCompanyApi[]
}

export interface MasterCompanyApi {
  name: string;
  lastName: string;
  document: string;
  salary: string;
  gender: string;
  position: string;
  startDate: string;
}
