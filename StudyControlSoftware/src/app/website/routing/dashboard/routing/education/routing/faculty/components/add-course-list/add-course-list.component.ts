import { AfterContentInit, Component, Input } from '@angular/core';

import { Course } from 'src/app/website/routing/dashboard/routing/education/models';

@Component({
  selector: 'app-dashboard-add-course-list',
  templateUrl: 'add-course-list.component.html',
  styleUrls: [ 'add-course-list.component.css' ]
})
export class AddCourseListComponent implements AfterContentInit {
  @Input() facultyIndex: number;

  addedCourses: Course[] = [
    new Course(1, 'C# ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C# ծրագրավորման լեզուն'),
    new Course(2, 'C++ ծրագրավորում', 'Սովորում ենք գրել ծրագրեր օգտագործելով C++ ծրագրավորման լեզուն'),
    new Course(3, 'ASP.NET Core', 'Սովորում ենք գրել ծրագրեր օգտագործելով ASP.NET Core framework-ը')
  ];

  ngAfterContentInit() {
    console.log(this.facultyIndex);
  }
}