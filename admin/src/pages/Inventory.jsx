import React from 'react';
import { GridComponent, ColumnsDirective, ColumnDirective, Page, Selection, Inject, Edit, Toolbar, Sort, Filter, Search } from '@syncfusion/ej2-react-grids';

import { inventoryData, inventoryGrid } from '../data/dummy';
import { Header } from '../components';

const Inventory = () => {
  const toolbarOptions = ['Search', 'Delete'];
  const editing = { allowDeleting: true, allowEditing: true };

  return (
    <div className="m-2 md:m-10 mt-24 p-2 md:p-10 bg-white rounded-3xl">
      <Header category="Page" title="Inventory" />
      <GridComponent
        dataSource={inventoryData}
        enableHover={false}
        allowPaging
        pageSettings={{ pageCount: 5 }}
        toolbar={toolbarOptions}
        editSettings={editing}
        allowSorting
        allowFiltering // Enabling filtering globally
        filterSettings={{ type: 'Excel' }} // 'Menu', 'CheckBox', 'Excel'
      >
        <ColumnsDirective>
          {/* Add column directives, ensuring the category field allows filtering */}
          {inventoryGrid.map((item, index) => (<ColumnDirective key={index} {...item} allowFiltering={item.field === 'CategoryName'} />))}
        </ColumnsDirective>
        <Inject services={[Page, Selection, Toolbar, Edit, Sort, Filter, Search]} />
      </GridComponent>
    </div>
  );
};
export default Inventory;
