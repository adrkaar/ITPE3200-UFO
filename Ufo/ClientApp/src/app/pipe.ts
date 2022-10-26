import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';


// Pipe for å kunne sende input tag fra ts til html
// Dette vil da kunne føre med sikkerhetshull
@Pipe({ name: 'safeHtml' })
export class SafeHtml implements PipeTransform {
    constructor(private sanitizer: DomSanitizer) { }

    transform(html) {
        //return this.sanitizer.bypassSecurityTrustStyle(html);
        return this.sanitizer.bypassSecurityTrustHtml(html);
        // return this.sanitizer.bypassSecurityTrustScript(html);
        // return this.sanitizer.bypassSecurityTrustUrl(html);
        // return this.sanitizer.bypassSecurityTrustResourceUrl(html);
    }
}