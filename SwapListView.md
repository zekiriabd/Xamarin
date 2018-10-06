# Xamarin

 #### SwapListView
 
 *  Add SwapListView.cs in your prtable project.
 *  Add SwapListViewRenderer.cs in your Droid.project
 *  Add SwapListViewRenderer.cs in your IOS.project
 *  Change your ListView in Xaml File :
 
```cpp
<ListView> 
    <ListView.ItemTemplate>
         <DataTemplate>
               <local:SwapViewCell>
                   <local:SwapViewCell.Content>
                      <ContentView>
                         // Code : Items Binding
                      </ContentView>
                   </local:SwapViewCell.Content>
                   <local:SwapViewCell.Menu>
                      <DataTemplate>
                       // Code : Swap Menu Binding
                      </DataTemplate>
                   </local:SwapViewCell.Menu>
               </local:SwapViewCell>
         </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

## Platform Support

|Platform|Version|
| ------------------- | :------------------: |
|Xamarin.iOS|iOS 8+|
|Xamarin.Android|API 14+|


<p align="center">
  <img src="https://i.imgur.com/fhdS1Hv.gif" alt="badges" style="margin:auto">
</p>


<p>
  https://www.youtube.com/watch?v=PuKdSNmYE1Y&list=PLfVdvKscEioMSVPWYUW0XD3CKKohUYMEG&index=14
</p>
