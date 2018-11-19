import { Directive, Output, ElementRef, EventEmitter, OnInit } from '@angular/core';


@Directive({
  selector: '[appDatepicker]'
})
export class DatepickerDirective implements OnInit {
  @Output() public change = new EventEmitter();

  constructor(private elemetRef: ElementRef) { }

  public ngOnInit(): void {
    $(this.elemetRef.nativeElement).datepicker({
      dateFormat: 'mm/dd/yy',
      changeYear: true,
      yearRange: '-100:+0',
      onSelect: (dateText) => {
        this.change.emit(dateText);
      }
    });
  }
}
