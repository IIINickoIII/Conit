class Company {
  final int id;
  final String name;
  final String description;
  final String contactName;
  final String phoneNumber;
  final String pictureId;

  Company._({
    this.id,
    this.name,
    this.description,
    this.contactName,
    this.phoneNumber,
    this.pictureId
});

  factory Company.fromJson(Map<String, dynamic> json) {
    return new Company._(
      id: json['id'],
      name: json['name'],
      description: json['description'],
      contactName: json['contactName'],
      phoneNumber: json['phoneNumber'],
      pictureId: json['pictureId']
    );
  }
}