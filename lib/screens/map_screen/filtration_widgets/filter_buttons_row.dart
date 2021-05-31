import 'package:flutter/material.dart';
import 'package:kampus_sggw/logic/event_parameters/filter_by_function_event_param.dart';
import 'package:kampus_sggw/models/map_item.dart';
import 'package:kampus_sggw/models/service.dart';
import 'package:kampus_sggw/translations/locale_keys.g.dart';
import 'package:easy_localization/easy_localization.dart';
import '../filter_button.dart';

class FilterButtonsRow extends StatelessWidget {
  final Function onButtonPressed;

  const FilterButtonsRow({
    Key key,
    @required this.onButtonPressed,
  }) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      scrollDirection: Axis.horizontal,
      child: Padding(
        padding: EdgeInsets.only(
          bottom: 15,
          top: 1,
          right: 10,
        ),
        child: Row(
          children: <Widget>[
            FilterButton(
              color: Color(0xFFf29900),
              icon: Icons.restaurant,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.food.tr(),
                  mapItemTypes: [MapItemType.food],
                  serviceTypes: [ServiceType.canteen],
                ),
              ),
            ),
            FilterButton(
              color: Color(0xFF1a73e8),
              icon: Icons.directions_bus_outlined,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.bus.tr(),
                  mapItemTypes: [MapItemType.transport],
                ),
              ),
            ),
            FilterButton(
              color: Color(0xFF7986CB),
              icon: Icons.local_parking_outlined,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.parking.tr(),
                  mapItemTypes: [MapItemType.parking],
                ),
              ),
            ),
            FilterButton(
              color: Colors.green,
              icon: Icons.park,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.park.tr(),
                  mapItemTypes: [MapItemType.monument],
                ),
              ),
            ),
            FilterButton(
              color: Color(0xFF5491f5),
              icon: Icons.storefront_outlined,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.store.tr(),
                  mapItemTypes: [MapItemType.store, MapItemType.medicine],
                  serviceTypes: [ServiceType.vendingMachine],
                ),
              ),
            ),
            FilterButton(
              color: Colors.indigo,
              icon: Icons.print_rounded,
              onTapFunction: () => onButtonPressed(
                FilterByFunctionEventParam(
                  filterName: LocaleKeys.copier.tr(),
                  serviceTypes: [ServiceType.copier],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
