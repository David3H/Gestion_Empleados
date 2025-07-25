import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmployeeDTO } from '../dto/employee.dto';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
 private api = 'http://localhost:5221/api/Employees';
  private http = inject(HttpClient);

  getAll(): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(this.api);
  }

  get(id: number): Observable<EmployeeDTO> {
    return this.http.get<EmployeeDTO>(`${this.api}/${id}`);
  }

  create(e: EmployeeDTO): Observable<any> {
    return this.http.post(this.api, e);
  }

  update(id: number, e: EmployeeDTO): Observable<any> {
    return this.http.put(`${this.api}/${id}`, e);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.api}/${id}`);
  }
}
