﻿using Contacts_MAUI.Interfaces;
using Contacts_MAUI.Mvvm.Models;
using Newtonsoft.Json;

namespace Contacts_MAUI.Services;

public class ContactService : IContactService
{
    private static readonly string filePath = @"/Users/markuskarlsson/Nackademin/cSharp/myProjects/Examination_cSharp/Contacts_MAUI/ListOfContacts.Json";
    private static List<ContactModel> contacts = new List<ContactModel>();
    private JsonService _jsonService = new JsonService();
    public static event Action ContactsUpdated;


    // Tar emot och lägger till ny kontakt i lista samt Json-fil
    public void AddToList(ContactModel contact)
    {
        contacts.Add(contact);
        WhenContactsUpdated();
    }

    // Hämtar alla kontakter från Json-fil och tilldelar i lokala listan
    public List<ContactModel> GetContacts()
    {
        var content = _jsonService.ReadFromJson(filePath);
        if (!string.IsNullOrEmpty(content))
            contacts = JsonConvert.DeserializeObject<List<ContactModel>>(content)!;

        return contacts;
    }

    // Tar bort kontakt från listan samt uppdaterar Json-fil och gränssnitt
    public void DeleteContact(ContactModel contact)
    {
        if (contact != null)
            contacts.Remove(contact);
            
        WhenContactsUpdated();
    }

    // Hämtar ut specifik kontakt från listan utefter Id
    public static ContactModel GetOneContact(string contactId)
    {
        if (Guid.TryParse(contactId, out var GuidcontactId))
            return contacts.FirstOrDefault(x => x.Id == GuidcontactId);

        return null;
    }

    // Skickar in uppdaterad kontakt och uppdaterar lokal lista samt Json-fil. 
    public void SaveUpdatedContact(ContactModel updatedContact)
    {
        // Kontakt uppdateras omgående när den kommer in i ContactService. Behöver endast skrivas mot .json samt uppdatera gränssnitt
        WhenContactsUpdated();
    }

    // Sparar ner ändringar till Json-fil samt uppdaterar gränssnitt via Invoke...
    protected virtual void WhenContactsUpdated()
    {
        _jsonService.SaveToJson(JsonConvert.SerializeObject(contacts), filePath);
        ContactsUpdated?.Invoke();
    }

}

