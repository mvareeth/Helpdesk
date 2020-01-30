import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class SubscriptionService {

    // Observable string sources
    private signedInAnnouncedSource = new Subject<boolean>();
    // Observable boolean sources
    private applicationHaltAnnouncedSource = new Subject<string>();

    // Observable string streams
    public signedInAnnounced$ = this.signedInAnnouncedSource.asObservable();
    // Observable boolean streams
    public applicationHaltAnnounced$ = this.applicationHaltAnnouncedSource.asObservable();

    // Service message commands
    public announceSignedIn(isSignedIn: boolean) {
        this.signedInAnnouncedSource.next(isSignedIn);
    }
    public announceApplicationHalt(halt: string) {
        this.applicationHaltAnnouncedSource.next(halt);
    }
}
