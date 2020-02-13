export class TicketModel {
    /// <summary>
    /// The Id for this ticket
    /// </summary>
    id?: number;

    /// <summary>
    /// The title of this ticket
    /// </summary>
    title: string;

    /// <summary>
    /// The description for this ticket
    /// </summary>
    description: string;

    /// <summary>
    /// status id decides whether it is opened, closed etc.
    /// </summary>
    statusId: number;

    /// <summary>
    /// list of technicians working on this ticket
    /// </summary>
    assigedTechnicianId?: number;
    /// <summary>
    /// client information of the ticket
    /// </summary>
    clientId: number;
}