class InstructionPart {
  final int id;
  final String pictureId;
  final int pageNumber;
  final int partId;

  InstructionPart._({
    this.id,
    this.pictureId,
    this.pageNumber,
    this.partId,
  });

  factory InstructionPart.fromJson(Map<String, dynamic> json) {
    return new InstructionPart._(
      id: json['id'],
      pictureId: json['pictureId'],
      pageNumber: json['pageNumber'],
      partId: json['partId'],
    );
  }
}