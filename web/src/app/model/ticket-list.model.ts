export interface TicketListModel {
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
    /// status id decides whether it is opened, closed etc.
    /// </summary>
    status: string;
    /// <summary>
    /// name of the person it is assigned currently
    /// </summary>
    assignedTo: string;
}
