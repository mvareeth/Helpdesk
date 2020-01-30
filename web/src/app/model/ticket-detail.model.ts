import { UserProfile } from './user-profile.model';

export interface TicketDetailModel {
    /// <summary>
    /// The Id for this ticket
    /// </summary>
    id: number;

    /// <summary>
    /// The title of this ticket
    /// </summary>
    title: string;

    /// <summary>
    /// The description for this ticket
    /// </summary>
    description: string;

    /// <summary>
    /// The complexity (1-3) of this ticket
    /// </summary>
    complexity: number;

    /// <summary>
    /// Defines priority level; if 1 that is needs to attend first
    /// </summary>
    priority: number;

    /// <summary>
    /// Notes for this ticket
    /// </summary>
    notes: string;

    /// <summary>
    /// The userId who created it
    /// </summary>
    createdBy: number;

    /// <summary>
    /// The created date
    /// </summary>
    createdDate: Date;

    /// <summary>
    /// The userId who updated last
    /// </summary>
    lastUpdatedBy: number;
    /// <summary>
    /// The last updated date
    /// </summary>
    lastUpdatedDate: Date;

    /// <summary>
    /// The date closed
    /// </summary>
    closedDate: Date;

    /// <summary>
    /// The user id who closed it
    /// </summary>
    closedBy: number;
    /// <summary>
    /// status id decides whether it is opened, closed etc.
    /// </summary>
    statusId: number;

    /// <summary>
    /// list of technicians working on this ticket
    /// </summary>
    technicians: UserProfile[];
    /// <summary>
    /// client information of the ticket
    /// </summary>
    client: string;
}