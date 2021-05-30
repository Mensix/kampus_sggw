import 'package:flutter/material.dart';
import 'package:easy_localization/easy_localization.dart';
import 'package:kampus_sggw/models/category.dart';
import 'package:kampus_sggw/models/map_item.dart';
import 'package:kampus_sggw/screens/map_screen/map_item_display/info_card.dart';
import 'package:kampus_sggw/translations/locale_keys.g.dart';
import 'package:kampus_sggw/screens/map_screen/map_item_display/category_item.dart';
import 'package:kampus_sggw/screens/map_screen/map_item_display/floor_tile.dart';
import 'package:kampus_sggw/screens/map_screen/map_item_display/service_button_row.dart';

class InfoCardDialogBuilder {
  String header;
  ListView description;
  ServiceButtonsRow servicesRow;
  String photoPath;
  MapItemType mapItemType;
  Text mapItemDescription;
  List<Image> mapItemGallery = [];
  List<Widget> otherCategories = [];
  Widget facultyTile;
  Widget instituteTile;
  String mapItemWebsite;

  InfoCardDialog fromMapItem(MapItem mapItem) {
    if (mapItem.gallery != null && mapItem.gallery.isNotEmpty) {
      for (var image in mapItem.gallery) {
        mapItemGallery.add(Image.network(image));
      }
    }

    if (mapItem.categories != null && mapItem.categories.isNotEmpty) {
      final data = mapItem.categories;
      for (Category category in data) {
        if (category.name == 'faculties') {
          facultyTile = CategoryItem(category);
        } else if (category.name == 'institutes') {
          instituteTile = CategoryItem(category);
        } else {
          for (Category subCategory in category.subCategories)
            otherCategories.add(CategoryItem(subCategory));
        }
      }
      description = _buildDescription();
    } else {
      description = ListView(
        shrinkWrap: true,
      );
    }

    if (mapItem.description != null) {
      mapItemDescription = Text(
        mapItem.description,
        textAlign: TextAlign.center,
        style: TextStyle(
            fontWeight: FontWeight.w500, fontSize: 16, fontFamily: 'SGGWSans'),
      );
    }

    if (mapItem.services != null && mapItem.services.isNotEmpty) {
      servicesRow = ServiceButtonsRow(mapItem.services);
    }

    if (mapItem.url != null) {
      mapItemWebsite = mapItem.url;
    }

    return InfoCardDialog(
      header: mapItem.name,
      subcategories: description,
      servicesRow: servicesRow,
      photoPath: mapItem.photoPath,
      mapItemType: mapItem.type,
      mapItemDescription: mapItemDescription,
      mapItemGallery: mapItemGallery,
      otherCategories: otherCategories,
      facultyTile: facultyTile,
      mapItemWebsite: mapItemWebsite,
    );
  }

  ListView _buildDescription() {
    return ListView.builder(
      shrinkWrap: true,
      itemCount: _descriptionItemsCount(),
      itemBuilder: _descriptionItemsBuilder,
    );
  }

  int _descriptionItemsCount() {
    int isFaculty = facultyTile != null ? 1 : 0;
    int isInstitute = instituteTile != null ? 1 : 0;
    int hasOtherCategories = otherCategories.length > 0 ? 1 : 0;

    return isFaculty + isInstitute + hasOtherCategories + 1;
  }

  Widget _descriptionItemsBuilder(BuildContext context, int index) {
    if (index == _descriptionItemsCount() - 1) {
      return ExpansionTile(
        title: Text(LocaleKeys.floor_plans.tr()),
        leading: Icon(Icons.map),
        children: [
          // TODO: load floors from mapItem
          FloorTile(
            Image.asset("assets/images/floors/floor_1.jpg"),
            LocaleKeys.floor.tr() + " I",
          ),
          FloorTile(
            Image.asset("assets/images/floors/floor_1.jpg"),
            LocaleKeys.floor.tr() + " II",
          ),
          FloorTile(
            Image.asset("assets/images/floors/floor_1.jpg"),
            LocaleKeys.floor.tr() + " III",
          ),
          FloorTile(
            Image.asset("assets/images/floors/floor_1.jpg"),
            LocaleKeys.floor.tr() + " IV",
          ),
        ],
      );
    }
    if (index == _descriptionItemsCount()) {
      if (otherCategories != null && otherCategories.isNotEmpty) {
        return ExpansionTile(
          leading: Icon(Icons.build_outlined),
          title: Text(LocaleKeys.other_institutions.tr()),
          children: otherCategories,
        );
      }
    }
    if (facultyTile != null && index == 0) return facultyTile;
    if (instituteTile != null && index == 1 ||
        facultyTile != null && index == 0) return instituteTile;
  }
}