import { Component, inject, OnInit } from '@angular/core';
import { NotificationService } from '../../../core/notifications/notification.service';
import { Router } from '@angular/router';
import { EmployeeDTO } from '../../dto/employee.dto';
import { EmployeeService } from '../../services/employee.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employee-list',
  imports: [CommonModule,FormsModule,FontAwesomeModule,],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit {
  constructor(library: FaIconLibrary) {
    library.addIcons(faEdit, faTrash);
  }
router = inject(Router);
notif = inject(NotificationService);
private service = inject(EmployeeService);

  employees: EmployeeDTO[] = [];
filter = '';
page = 1;
perPage = 10;
pages: number[] = [];

ngOnInit() { this.load(); }

load() {
  this.service.getAll().subscribe(list => {
    const filt = list.filter(e => e.firstName.includes(this.filter) || e.lastName.includes(this.filter) || e.email.includes(this.filter));
    this.pages = Array(Math.ceil(filt.length / this.perPage)).fill(0);
    this.employees = filt.slice((this.page-1)*this.perPage, this.page*this.perPage);
  });
}

create() { this.router.navigate(['/create']); }

edit(id: number) { this.router.navigate(['/edit', id]); }

 delete(id?: number) {
    if (id == null) return;
    if (!confirm('¿Decea eliminar de forma permanente este empleado?')) return;
    this.service.delete(id).subscribe({
      next: () => {
        this.notif.success('Empleado eliminado correctamente');
        // Si eliminas el último item de la página actual y ya no hay items, volver atrás
        if (this.employees.length === 1 && this.page > 1) {
          this.page--;
        }
        this.load();
      },
      error: (err: HttpErrorResponse) => this.notif.error(err.error.message || err.message),
    });
  }

  changePage(p: number) {
    this.page = p;
    this.load();
  }
}
